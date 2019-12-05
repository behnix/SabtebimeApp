namespace App.Core.Convertors
{
    public static class Price
    {
        public static string ToPrice(this object dec)
        {
            var src = dec.ToString();
            src = src.Replace(".0000", "");
            if (!src.Contains("."))
            {
                src = src + ".00";
            }
            var price = src.Split('.');
            if (price[1].Length >= 2)
            {
                price[1] = price[1].Substring(0, 2);
                price[1] = price[1].Replace("00", "");
            }

            string temp = null;
            var i = 0;
            if ((price[0].Length % 3) >= 1)
            {
                temp = price[0].Substring(0, (price[0].Length % 3));
                for (i = 0; i <= (price[0].Length / 3) - 1; i++)
                {
                    temp += "," + price[0].Substring((price[0].Length % 3) + (i * 3), 3);
                }
            }
            else
            {
                for (i = 0; i <= (price[0].Length / 3) - 1; i++)
                {
                    temp += price[0].Substring((price[0].Length % 3) + (i * 3), 3) + ",";
                }

                if (temp != null) temp = temp.Substring(0, temp.Length - 1);
            }
            if (price[1].Length > 0)
            {
                return temp + "." + price[1];
            }
            else
            {
                return temp;
            }
        }
    }
}
