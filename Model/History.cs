namespace MyCollection_0._1.Model
{
    public class History
    {
        public int Id { get; set; } // Identifiant unique de l'historique
        public string Action { get; set; } // Type d'action : Add, Update, Delete, etc.
        public string ItemName { get; set; } // Le nom de l'élément concerné
        public DateTime Timestamp { get; set; } // La date et l'heure de l'action
    }
}
