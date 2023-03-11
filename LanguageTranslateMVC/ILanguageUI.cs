using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageTranslateMVC
{
    public interface ILanguageUI

    {
        void Welcome();
        void Options();
        void AskText();
        void DetectLanguage();
        void AskTargetLang();
        void AskSourceLang();
    }
}
