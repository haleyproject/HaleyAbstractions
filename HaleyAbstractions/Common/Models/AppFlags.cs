using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haley.Models {
    public class AppFlags {
        public bool DevEnvironment { get; set; }
        public bool Mock { get; set; } = true; //By default Mock is always true //We control few things like emails etc, and modify the settings accordingly. 
        public bool ConsoleLog { get; set; }
        public bool Debug { get; set; }
        //GlassBreak is a special flag that is used to break the glass in case of emergency. It is used to bypass the mock and other settings.
        //It has no public setter on purpose: it can never arrive from appsettings, an environment variable or a configuration binder.
        //A developer breaks the glass in code with BreakGlass() and removes that line before deployment.
        public bool GlassBreak { get; private set; }
        //Why the glass was broken. Written to the host log so the reason survives with the deployment.
        public string GlassBreakReason { get; private set; } = string.Empty;
        public bool ThrowExceptions { get; set; }

        //Deliberate, hardcoded emergency switch. The reason is required so a stray call is never anonymous.
        public AppFlags BreakGlass(string reason) {
            if (string.IsNullOrWhiteSpace(reason)) throw new ArgumentException("State why the glass is being broken.", nameof(reason));
            GlassBreak = true;
            GlassBreakReason = reason.Trim();
            return this;
        }

        public override string ToString() {
            return $@"Dev={DevEnvironment} | Mock={Mock} | ConsoleLog={ConsoleLog} | Debug={Debug} | ThrowExceptions={ThrowExceptions} | GlassBreak={GlassBreak}";
        }
    }
}
