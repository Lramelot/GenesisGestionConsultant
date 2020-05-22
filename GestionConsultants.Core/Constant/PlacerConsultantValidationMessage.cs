namespace GestionConsultants.Core.Constant
{
    public class PlacerConsultantValidationMessage
    {
        public static string PosteInterneNonRenseigne => "L'intitulé du poste interne doit être renseigné";
        public static string ConsultantIntrouvable => "Le consultant renseigné ne semble pas exister";
        public static string MissionIntrouvable => "La mission renseignée ne semble pas exister";
        public static string RateNonRenseigne => "Le rate doit être renseigné";
        public static string RateTropEleve => "Le rate demandé est trop élevé";
        public static string ExperienceNonSuffisante => "L'expérience du consultant n'est pas suffisante";
    }
}