using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGLib.Skill
{
    public class Skills
    {
        //KOPF
        public int Science { get; set; }
        public int Logic { get; set; }
        public int Senses { get; set; }
        //KÖRPER
        public int Strength { get; set; }
        public int Speed { get; set; }
        public int Health { get; set; }
        //GEIST
        public int Rhetoric { get; set; }
        public int HumanKnowledge { get; set; }
        public int Acting { get; set; }

        #region Methods

        #endregion

        #region Indexer

        public int this[string name]
        {
            get
            {
                switch (name)
                {
                    case "SC":
                        return Science;
                    case "LO":
                        return Logic;
                    case "SE":
                        return Senses;
                    case "ST":
                        return Strength;
                    case "SP":
                        return Speed;
                    case "HE":
                        return Health;
                    case "CH":
                        return Rhetoric;
                    case "HU":
                        return HumanKnowledge;
                    case "AC":
                        return Acting;
                    default:
                        throw new ArgumentException("Skill not found");
                }
            }
            set
            {
                switch (name)
                {
                    case "SC":
                        Science = value;
                        break;
                    case "LO":
                        Logic = value;
                        break;
                    case "SE":
                        Senses = value;
                        break;
                    case "ST":
                        Strength = value;
                        break;
                    case "SP":
                        Speed = value;
                        break;
                    case "HE":
                        Health = value;
                        break;
                    case "CH":
                        Rhetoric = value;
                        break;
                    case "HU":
                        HumanKnowledge = value;
                        break;
                    case "AC":
                        Acting = value;
                        break;
                    default:
                        throw new ArgumentException("Skill not found");
                }
            }
        }

        #endregion
    }
}
