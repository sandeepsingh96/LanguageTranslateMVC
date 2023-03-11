using System;
using System.Threading.Tasks;

namespace LanguageTranslateMVC
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            

            ILanguageUI GivernUI = new LanguageUI();
            ILanguageTranslatorModel GivenModel = new LanguageTranslatorModel();
            ILanguageController Controller = new LanguageController(GivenModel,GivernUI);


          await Controller.ControllerStart();

            






        }
    }
}
