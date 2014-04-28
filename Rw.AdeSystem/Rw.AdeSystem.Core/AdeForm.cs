#region Using directives

using System;

#endregion

namespace Rw.AdeSystem.Core
{
    public class AdeForm
    {
        public AdeForm(string formString)
        {
            ParseFromString(formString);
        }

        private void ParseFromString(string formString)
        {
            throw new NotImplementedException();
        }

        public string ToPrologString()
        {
            throw new NotImplementedException();
        }
    }
}