using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vapor.Models
{
    public class Item
    {
        static public string KeyGen()
        {
            Random r = new Random();
            var key = "";
            var alph = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            for (int i = 0; i < 5; i++)
                key += alph[r.Next(0, alph.Length)];
            return key;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Sale { get; set; } = 0.0;
        public decimal Price { get; set; }
        public string Img { get; set; }
        public string Img2 { get; set; }
        public string Img3 { get; set; }
        public string Img4 { get; set; }
        public string Img5 { get; set; }
        public string Vid { get; set; }
        public long AvgScore { get; set; }
        public long ScoreSumm { get; set; }
        public long ScoreCount { get; set; }
        public string Tag1 { set; get; }
        public string Tag2 { set; get; }
        public string Tag3 { set; get; }

        public List<Review> Reviews { get; set; }

        public override string ToString()
        {
            return $"{Id}";
        }
        public decimal SalePrice()
        {
            return (decimal)((double)Price - (double)Price * Sale);
        }

        public void UpdateAvgScore()
        {
            if (ScoreCount == 0)
            {
                AvgScore = -1;
                return;
            }

            AvgScore = ScoreSumm / ScoreCount;
        }
    }
}
