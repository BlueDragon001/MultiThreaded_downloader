using System;

namespace MultiThreaded_downloader
{
    class Program
    {
        static void Main(string[] args)
        {

            String nPath = "https://rr5---sn-oppb-3uhz.googlevideo.com/videoplayback?expire=1641006616&ei=uHHPYd-OLuKdg8UP2ce5gAM&ip=163.53.24.124&id=o-ACI-OjSLRbLJtAYtTxtKPbAk1PIVwZG-MdY6fgTpYRpr&itag=247&aitags=133%2C134%2C135%2C136%2C160%2C242%2C243%2C244%2C247%2C278%2C394%2C395%2C396%2C397%2C398&source=youtube&requiressl=yes&mh=qv&mm=31%2C26&mn=sn-oppb-3uhz%2Csn-npoe7nek&ms=au%2Conr&mv=m&mvi=5&pl=24&initcwndbps=496250&vprv=1&mime=video%2Fwebm&ns=45PxVJANohoAW4u6L5PS1mQG&gir=yes&clen=33074614&dur=320.066&lmt=1621093423916547&mt=1640984683&fvip=5&keepalive=yes&fexp=24001373%2C24007246&c=WEB&txp=5432432&n=J-wPaRRoERLYvvTL&sparams=expire%2Cei%2Cip%2Cid%2Caitags%2Csource%2Crequiressl%2Cvprv%2Cmime%2Cns%2Cgir%2Cclen%2Cdur%2Clmt&lsparams=mh%2Cmm%2Cmn%2Cms%2Cmv%2Cmvi%2Cpl%2Cinitcwndbps&lsig=AG3C_xAwRAIgRasnMNGCgmkJED4fHHhFEVPXroXBdPqafK8g75sP2qoCIBHd8DJnc_1oxcQHMiYWGnTX-udGAKnC7USyDtuvoyMm&sig=AOq0QJ8wRQIgfKuJcyW3y4vFKD09II5Ciwb2E33rGUcKyccNG6YASL0CIQDLQwPxeDGuYqYwdVYb70N_siX_UyCLu63IPuy11BS9HQ==";
            String graphic = "http://www.cs.ucy.ac.cy/courses/EPL426/courses/eBooks/ComputerGraphicsPrinciplesPractice.pdf";
            String path = "https://images.pexels.com/photos/1591447/pexels-photo-1591447.jpeg?cs=srgb&dl=pexels-guillaume-meurice-1591447.jpg&fm=jpg";

            Console.WriteLine("Writing Data"+"("+ ">_<"+")");
            
            String[] disposablePaths = { @"C:\Download\temp1", @"C:\Download\temp2" , @"C:\Download\temp3" };
            String Destination = @"C:\Download\tw.";
            var nd = new NewDownloader(graphic, disposablePaths, Destination);
            nd.Download();
            

        }

        
    }
}
    