using System;
using System.Collections.Generic;
using System.Text;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IBotFeedback
    {
        IAspNetUsers Author { get; }
        bool AskTriggered { get; set; }
        string Type { get; set; }
        short? Rating { get; set; }
        string Comment { get; set; }
    }
}
