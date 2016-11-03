using System;

namespace BingDesktop.Misc
{
    public static class Header
    {
        public static void Print()
        {
            string headline = @" __      __            ___    ___                                              
/\ \  __/\ \          /\_ \  /\_ \                                             
\ \ \/\ \ \ \     __  \//\ \ \//\ \    _____      __     _____      __   _ __  
 \ \ \ \ \ \ \  /'__`\  \ \ \  \ \ \  /\ '__`\  /'__`\  /\ '__`\  /'__`\/\`'__\
  \ \ \_/ \_\ \/\ \L\.\_ \_\ \_ \_\ \_\ \ \L\ \/\ \L\.\_\ \ \L\ \/\  __/\ \ \/ 
   \ `\___x___/\ \__/.\_\/\____\/\____\\ \ ,__/\ \__/.\_\\ \ ,__/\ \____\\ \_\ 
    '\/__//__/  \/__/\/_/\/____/\/____/ \ \ \/  \/__/\/_/ \ \ \/  \/____/ \/_/ 
                                         \ \_\             \ \_\               
                                          \/_/              \/_/     ";

            var nl = Environment.NewLine;
            var cross =
"                  |          " + nl +
"              \\       /      " + nl +
"                .---.        " + nl +
"           '-.  |   |  .-'   " + nl +
"             ___|   |___     " + nl +
"        -=  [           ]  =-" + nl +
"            `---.   .---'    " + nl +
"         __||__ |   | __||__ " + nl +
"         '-..-' |   | '-..-' " + nl +
"           ||   |   |   ||   " + nl +
"           ||_.-|   |-,_||   " + nl +
"         .-\"`   `\"`'`   `\"-. " + nl +
"       .'                   '";

            var copyright = "powered by ittner.it";


            Console.WriteLine(headline + nl + cross + nl + nl + copyright + nl + nl);
        }
    }
}
