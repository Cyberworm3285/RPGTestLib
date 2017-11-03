using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGLib.Skill
{
    public class SkillManager
    {
        private static SkillManager _instance;
        public static SkillManager Instance => _instance ?? (_instance = new SkillManager());

        public Skills Skills { get; private set; }

        #region Constructors

        private SkillManager() { }

        #endregion

        #region Methods

        #endregion
    }
}
