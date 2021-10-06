using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vapor.Models
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Img { get; set; }
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
    }
}
