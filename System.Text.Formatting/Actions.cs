using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Operations;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace System.Text.Formatting
{
    internal class Actions
    {
        public static void OpenURL(object sender, RequestNavigateEventArgs e) => _ = Execute.Url(((Hyperlink)sender).NavigateUri);
    }
}
